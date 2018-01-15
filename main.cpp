#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <iostream>
#include <cstdlib>
#include "opencv2/imgcodecs.hpp"

using namespace cv;
using namespace std;

Mat img;
Mat imgGray;
int thresh = 100;
int max_thresh = 255;
RNG rng(12345);

void thresh_callback(int, void* );

int main() {

//    img= imread("./figures.jpg", 3);
    VideoCapture cap(10); //capture the video from web cam

    if (!cap.isOpened())  // if not success, exit program
    {
        cout << "Cannot open the web cam" << endl;
        return -1;
    }

    do{
        cap.grab();
        cap.retrieve(img);
        const char* source_window = "Source";
        namedWindow( source_window, WINDOW_AUTOSIZE );
        imshow( source_window, img );

//        namedWindow("Original", 0);
//        resizeWindow("Original", 500, 500);
//        imshow("Original", img);


        cvtColor( img, imgGray, COLOR_BGR2GRAY );
        blur( imgGray, imgGray, Size(3,3) );

        namedWindow("Gray", 0);
        resizeWindow("Gray", 500, 500);
        imshow("Gray", imgGray);

        createTrackbar( " Canny thresh:", "Source", &thresh, max_thresh, thresh_callback );
        thresh_callback( 0, 0 );

        if (waitKey(30) == 27) {
            cout << "esc key is pressed by user" << endl;
            break;
        }
    }while(true);


    return 0;
}


void thresh_callback(int, void* )
{
    Mat canny_output;
    vector<vector<Point> > contours;
    vector<Vec4i> hierarchy;
    Canny( imgGray, canny_output, thresh, thresh*2, 3 );
    findContours( canny_output, contours, hierarchy, RETR_TREE, CHAIN_APPROX_SIMPLE, Point(0, 0) );
    Mat drawing = Mat::zeros( canny_output.size(), CV_8UC3 );
    for( size_t i = 0; i< contours.size(); i++ )
    {


        Scalar color = Scalar( rng.uniform(0, 255), rng.uniform(0,255), rng.uniform(0,255) );
        drawContours( drawing, contours, (int)i, color, 2, 8, hierarchy, 0, Point() );
    }
    namedWindow( "Contours", WINDOW_AUTOSIZE );
    imshow( "Contours", drawing );
}