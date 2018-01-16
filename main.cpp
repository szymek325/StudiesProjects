#include "opencv2/highgui/highgui.hpp"
#include "opencv2/imgproc/imgproc.hpp"
#include <iostream>
#include <cstdlib>
#include "opencv2/imgcodecs.hpp"
#include "constants.h"

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
void WriteColorTextOnImage(vector<vector<Point>> someFigures);
String GetColor(Mat maska);
void Morphos(Mat &workImage);
int GetContourSIzeFromSpecificColorImage(Mat specificColorImage);


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
        WriteColorTextOnImage(foundFigures);






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

void WriteColorTextOnImage(vector<vector<Point>> someFigures)
{
    vector<vector<Point> > contours_poly(someFigures.size());
    vector<float> radius(someFigures.size());
    vector<Point2f> center(someFigures.size());

    for(size_t i = 0; i< someFigures.size(); i++ )
    {
        approxPolyDP(someFigures[i], contours_poly[i], arcLength(Mat(someFigures[i]), true)*0.02, true);
        minEnclosingCircle(contours_poly[i], center[i], radius[i]);


        Mat mask = Mat::zeros(canny_output.rows, canny_output.cols, CV_8UC1);
        drawContours(mask, someFigures,i, Scalar(255), CV_FILLED);
        String colorText=GetColor(mask);
        putText(img, colorText, center[i], FONT_HERSHEY_COMPLEX_SMALL, 0.8, boundingColor, 1,CV_AA);
    }

}

String GetColor(Mat maska)
{
    int contourSize=0;
    Mat crop(img.rows, img.cols, CV_8UC3);
    crop.setTo(Scalar(255,255,255));
    img.copyTo(crop, maska);
    normalize(maska.clone(), maska, 0.0, 255.0, CV_MINMAX, CV_8UC1);
    Mat imgHSV;
    cvtColor(crop, imgHSV, COLOR_BGR2HSV); //Convert the captured frame from BGR to HSV


    Mat imgRed;
    Mat redMask1, redMask2;
    inRange(imgHSV, Scalar(0, 100, 100), Scalar(10, 255, 255), redMask1); //Threshold the image
    inRange(imgHSV, Scalar(160, 100, 100), Scalar(179, 255, 255), redMask2); //Threshold the image
    cv::addWeighted(redMask1, 1.0, redMask2, 1.0, 0.0, imgRed);
    Morphos(imgRed);
    contourSize=GetContourSIzeFromSpecificColorImage(imgRed);
    if(contourSize!=0)
    {
        return "RED";
    }

    Mat imgBlue;
    inRange(imgHSV, Scalar(iBlueLowH, iBlueLowS, iBlueLowV), Scalar(iBlueHighH, iBlueHighS, iBlueHighV),
            imgBlue); //Threshold the image
    Morphos(imgBlue);
    contourSize=GetContourSIzeFromSpecificColorImage(imgBlue);
    if(contourSize!=0)
    {
        return "BLUE";
    }

    Mat imgYellow;
    inRange(imgHSV, Scalar(iYellowLowH, iYellowLowS, iYellowLowV), Scalar(iYellowHighH, iYellowHighS, iYellowHighV),
            imgYellow); //Threshold the image
    Morphos(imgYellow);
    contourSize=GetContourSIzeFromSpecificColorImage(imgYellow);
    if(contourSize!=0)
    {
        return "YELLOW";
    }

    Mat imgGreen;
    inRange(imgHSV, Scalar(iGreenLowH, iGreenLowS, iGreenLowV), Scalar(iGreenHighH, iGreenHighS, iGreenHighV),
            imgGreen); //Threshold the image
    Morphos(imgGreen);
    contourSize=GetContourSIzeFromSpecificColorImage(imgGreen);
    if(contourSize!=0)
    {
        return "GREEN";
    }

    return "COLOR UNRECOGNIZED";


//    namedWindow( "MASK", WINDOW_AUTOSIZE );
//    imshow( "MASK", maska );
//
//    namedWindow( "CROP", WINDOW_AUTOSIZE );
//    imshow( "CROP", crop );
//    return "text";
}

void Morphos(Mat &workImage){
    Mat erodeElement= getStructuringElement(MORPH_RECT, Size(3, 3));
    Mat dilateElement=getStructuringElement(MORPH_RECT, Size(8, 8));
    erode(workImage,workImage,erodeElement);
    erode(workImage,workImage,erodeElement);
    dilate(workImage,workImage,dilateElement);
    dilate(workImage,workImage,dilateElement);
}

int GetContourSIzeFromSpecificColorImage(Mat specificColorImage)
{
    vector<vector<Point> > contours;
    vector<Vec4i> hierarchy;
    vector<Vec3f> circles;

    threshold(specificColorImage, specificColorImage, thresh, 255, THRESH_BINARY);
    findContours(specificColorImage, contours, hierarchy, RETR_TREE, CV_CHAIN_APPROX_SIMPLE, Point(0, 0));

    int biggestArea=0;

    for (size_t i = 0; i < contours.size(); i++)
    {
        int area=(int)contourArea(contours[i],false);
        if(area>biggestArea)
        {
            biggestArea=area;
        }
    }

    return biggestArea;
}