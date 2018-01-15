#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <iostream>
#include <cstdlib>
#include "opencv2/imgcodecs.hpp"

using namespace cv;
using namespace std;

Mat img;
Mat imgGray;
int thresh = 52;
int max_thresh = 255;
RNG rng(12345);

void detectContours(int, void* );
void detectCircles(int,void*);

int main() {
    img= imread("./figures.jpg", 3);
    do{
        const char* source_window = "Source";
        namedWindow( source_window, WINDOW_AUTOSIZE );
        imshow( source_window, img );

        createTrackbar( " Canny thresh:", "Source", &thresh, max_thresh, detectContours );
        detectContours( 0, 0 );
        detectCircles(0,0);

        if (waitKey(30) == 27) {
            cout << "esc key is pressed by user" << endl;
            break;
        }
    }while(true);

    return 0;
}


void detectContours(int, void* ) //detect and draw contours
{

    Mat contoursImg;
    Mat canny_output;
    vector<vector<Point> > contours;
    vector<Vec4i> hierarchy;

    cvtColor( img, contoursImg, COLOR_BGR2GRAY );
    blur( contoursImg, contoursImg, Size(3,3) );
    Canny( contoursImg, canny_output, thresh, thresh*2, 3 );
    findContours( canny_output, contours, hierarchy, RETR_TREE, CHAIN_APPROX_SIMPLE, Point(0, 0) );

    Mat drawing = Mat::zeros( canny_output.size(), CV_8UC3 );

    vector<vector<Point> > contours_poly(contours.size());

    for (size_t i = 0; i < contours.size(); i++) {
        approxPolyDP(contours[i], contours_poly[i], arcLength(Mat(contours[i]), true)*0.02, true);
    }

    for( size_t i = 0; i< contours_poly.size(); i++ )
    {
        if(contours_poly[i].size()==5){
            Scalar color = Scalar( rng.uniform(0, 255), rng.uniform(0,255), rng.uniform(0,255) );
            drawContours( drawing, contours_poly, (int)i, color, 2, 8, hierarchy, 0, Point() );
        }
        else if(contours_poly[i].size()==4){
            Scalar color = Scalar( rng.uniform(0, 255), rng.uniform(0,255), rng.uniform(0,255) );
            drawContours( drawing, contours_poly, (int)i, color, 2, 8, hierarchy, 0, Point() );
        }
        else if(contours_poly[i].size()==3) {
            Scalar color = Scalar(rng.uniform(0, 255), rng.uniform(0, 255), rng.uniform(0, 255));
            drawContours(drawing, contours_poly, (int) i, color, 2, 8, hierarchy, 0, Point());
        }

    }

    namedWindow( "Contours", WINDOW_AUTOSIZE );
    imshow( "Contours", drawing );
}

void detectCircles(int,void*)
{
    Mat circlesImage;
    cvtColor( img, circlesImage, COLOR_BGR2GRAY );

    GaussianBlur( circlesImage, circlesImage, Size(9, 9), 2, 2 );

    vector<Vec3f> circles;

    /// Apply the Hough Transform to find the circles
    HoughCircles( circlesImage, circles, HOUGH_GRADIENT, 1, circlesImage.rows/8, 200, 100, 0, 0 );

    /// Draw the circles detected
    for( size_t i = 0; i < circles.size(); i++ )
    {
        Point center(cvRound(circles[i][0]), cvRound(circles[i][1]));
        int radius = cvRound(circles[i][2]);
        // circle center
        circle( circlesImage, center, 3, Scalar(0,255,0), -1, 8, 0 );
        // circle outline
        circle( circlesImage, center, radius, Scalar(0,0,255), 3, 8, 0 );
    }

    /// Show your results
    namedWindow( "Hough Circle Transform Demo", WINDOW_AUTOSIZE );
    imshow( "Hough Circle Transform Demo", circlesImage );

    waitKey(0);


}