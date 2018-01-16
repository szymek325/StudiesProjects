#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <iostream>
#include <cstdlib>
#include "opencv2/imgcodecs.hpp"

using namespace cv;
using namespace std;

Mat img;
Mat canny_output;
int thresh = 52;
int max_thresh = 255;
RNG rng(12345);
Scalar boundingColor = Scalar((0, 0), (0, 0), (0, 0));
vector<vector<Point> > foundFigures;

vector<vector<Point>> GetFigures(int, void *);
void detectCircles(int,void*);
void GetColors(vector<vector<Point>> someFigures);


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

        foundFigures=GetFigures(0, 0);
        printf("foundFigures %d",(int)foundFigures.size());
        GetColors(foundFigures);






        const char* source_window = "Source";
        namedWindow( source_window, WINDOW_AUTOSIZE );
        imshow( source_window, img );

        if (waitKey(30) == 27) {
            cout << "esc key is pressed by user" << endl;
            break;
        }
    }while(true);

    return 0;
}


vector<vector<Point>> GetFigures(int, void *) //detect and draw contours
{
    Mat contoursImg;
    vector<vector<Point> > contours;
    vector<vector<Point> > figures;
    vector<Vec4i> hierarchy;

    cvtColor( img, contoursImg, COLOR_BGR2GRAY );
    blur( contoursImg, contoursImg, Size(3,3) );
    Canny( contoursImg, canny_output, thresh, thresh*2, 3 );
    findContours( canny_output, contours, hierarchy, CV_RETR_EXTERNAL, CV_CHAIN_APPROX_NONE, Point(0, 0) );

    //Mat drawing = Mat::zeros( canny_output.size(), CV_8UC3 );
    //Mat mask = Mat::zeros(canny_output.rows, canny_output.cols, CV_8UC1);

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
                putText(img, "CIRCLE", center[i], FONT_HERSHEY_COMPLEX_SMALL, 0.8, boundingColor, 1,CV_AA);
                drawContours( img, contours, (int)i, boundingColor, 10, 8, hierarchy, 0, Point() );
                figures.push_back(contours[i]);
            }
            if(contours_poly[i].size()==5){
                putText(img, "PENTAGON", center[i], FONT_HERSHEY_COMPLEX_SMALL, 0.8, boundingColor, 1,CV_AA);
                drawContours( img, contours, (int)i, boundingColor, 10, 8, hierarchy, 0, Point() );
                figures.push_back(contours[i]);
            }
            else if(contours_poly[i].size()==4){
                putText(img, "RECTANGLE", center[i], FONT_HERSHEY_COMPLEX_SMALL, 0.8, boundingColor, 1,CV_AA);
                drawContours( img, contours, (int)i, boundingColor, 10, 8, hierarchy, 0, Point() );
                figures.push_back(contours[i]);
            }
            else if(contours_poly[i].size()==3) {
                putText(img, "TRIANGLE", center[i], FONT_HERSHEY_COMPLEX_SMALL, 0.8, boundingColor, 1,CV_AA);
                drawContours( img, contours, (int)i, boundingColor, 10, 8, hierarchy, 0, Point() );
                figures.push_back(contours[i]);
            }
        }
    }
    return figures;
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

void GetColors(vector<vector<Point>> someFigures)
{
    Mat mask = Mat::zeros(canny_output.rows, canny_output.cols, CV_8UC1);

    for(size_t i = 0; i< someFigures.size(); i++ )
    {
        drawContours(mask, someFigures,i, Scalar(255), CV_FILLED);
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

}