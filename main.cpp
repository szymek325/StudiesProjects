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
Scalar boundingColor = Scalar((0, 0), (0, 0), (0, 0));

void detectAndDrawContours(int, void *);
void detectCircles(int,void*);

void DrawContoursForFigure(const vector<vector<Point>> &contours, const vector<Vec4i> &hierarchy, const Mat &drawing,
                           const Mat &mask, const vector<vector<Point>> &contours_poly, const vector<Point2f> &center,
                           size_t i);

int main() {
    VideoCapture cap(10); //capture the video from web cam
    if (!cap.isOpened())  // if not success, exit program
    {
        cout << "Cannot open the web cam" << endl;
        return -1;
    }
//    img= imread("./figures.jpg", 3);

    do{
        cap.grab();
        cap.retrieve(img);
        const char* source_window = "Source";
        namedWindow( source_window, WINDOW_AUTOSIZE );
        imshow( source_window, img );

        createTrackbar(" Canny thresh:", "Source", &thresh, max_thresh, detectAndDrawContours);
        detectAndDrawContours(0, 0);
        //detectCircles(0,0);

        if (waitKey(30) == 27) {
            cout << "esc key is pressed by user" << endl;
            break;
        }
    }while(true);

    return 0;
}


void detectAndDrawContours(int, void *) //detect and draw contours
{
    Mat contoursImg;
    Mat canny_output;
    vector<vector<Point> > contours;
    vector<Vec4i> hierarchy;

    cvtColor( img, contoursImg, COLOR_BGR2GRAY );
    blur( contoursImg, contoursImg, Size(3,3) );
    Canny( contoursImg, canny_output, thresh, thresh*2, 3 );
    findContours( canny_output, contours, hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_NONE, Point(0, 0) );

    Mat drawing = Mat::zeros( canny_output.size(), CV_8UC3 );
    Mat mask = Mat::zeros(canny_output.rows, canny_output.cols, CV_8UC1);

    vector<vector<Point> > contours_poly(contours.size());
    vector<float> radius(contours.size());
    vector<Point2f> center(contours.size());

    for (size_t i = 0; i < contours.size(); i++) {
        approxPolyDP(contours[i], contours_poly[i], arcLength(Mat(contours[i]), true)*0.02, true);
        minEnclosingCircle(contours_poly[i], center[i], radius[i]);
    }

    for( size_t i = 0; i< contours_poly.size(); i++ )
    {
        if(isContourConvex(contours_poly[i])&&(int)contourArea(contours_poly[i],false)>1000&&hierarchy[i][2] < 0 && hierarchy[i][3] < 0){
            if(contours_poly[i].size()==8){
                DrawContoursForFigure(contours, hierarchy, drawing, mask, contours_poly,center, i);
            }
            if(contours_poly[i].size()==5){
                DrawContoursForFigure(contours, hierarchy, drawing, mask, contours_poly,center, i);
            }
            else if(contours_poly[i].size()==4){
                DrawContoursForFigure(contours, hierarchy, drawing, mask, contours_poly,center, i);
            }
            else if(contours_poly[i].size()==3) {
                DrawContoursForFigure(contours, hierarchy, drawing, mask, contours_poly,center, i);
            }
        }
    }



    Mat crop(img.rows, img.cols, CV_8UC3);

    // set background to green
    crop.setTo(Scalar(255,255,255));

    // and copy the magic apple
    img.copyTo(crop, mask);

    // normalize so imwrite(...)/imshow(...) shows the mask correctly!
    normalize(mask.clone(), mask, 0.0, 255.0, CV_MINMAX, CV_8UC1);

    namedWindow( "MASK", WINDOW_AUTOSIZE );
    imshow( "MASK", mask );

    namedWindow( "CROP", WINDOW_AUTOSIZE );
    imshow( "CROP", crop );

    namedWindow( "Contours", WINDOW_AUTOSIZE );
    imshow( "Contours", drawing );
}

void DrawContoursForFigure(const vector<vector<Point>> &contours, const vector<Vec4i> &hierarchy, const Mat &drawing,
                           const Mat &mask, const vector<vector<Point>> &contours_poly, const vector<Point2f> &center,
                           size_t i) {
    Scalar color = Scalar(rng.uniform(0, 255), rng.uniform(0, 255), rng.uniform(0, 255) );
    drawContours( drawing, contours_poly, (int)i, color, 2, 8, hierarchy, 0, Point() );
    putText(img, "CIRCLE", center[i], FONT_HERSHEY_COMPLEX_SMALL, 0.8, boundingColor, 1,CV_AA);

    drawContours(mask, contours,i, Scalar(255), CV_FILLED);
}

void detectCircles(int,void*) //with Hough
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




}