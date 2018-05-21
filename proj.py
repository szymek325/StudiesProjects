import cv2
import numpy as np
from PIL import Image
import glob, os

size = 20, 20

for x in range(1, 6):
    #przygotowanie nazw
    obr=""
    obr=str(x)+".png"
    gray=""
    gray="g"+str(x)+".png"
    binary=""
    binary="b"+str(x)+".png"

    #wczytanie i b-w
    image = cv2.imread(obr)
    gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    cv2.imwrite(gray,gray_image)

    #resize b-w
    im = Image.open(gray)
    im.thumbnail(size)
    obr='z'+obr
    im.save(obr)

    imb = cv2.imread(obr)
    ret,binary_image = cv2.threshold(imb,100,255, cv2.THRESH_BINARY)
    cv2.imwrite(binary,binary_image)

    cv2.imshow('binary_image',binary_image)    
    cv2.imshow('color_image',image)
    
    #usuwanie przejsciowych
    os.remove(obr)
    os.remove(gray)

    cv2.waitKey(0)                
    cv2.destroyAllWindows()

