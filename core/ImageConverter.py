import os

import cv2
from PIL import Image

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory

size = 20, 20
class ImageConverter:
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.temporaryDirectory = self.config.temporary_files_path

    def convert_to_binary(self, file_path):
        file_name = file_path.split('/')[-1]
        gray_file_path = os.path.join(self.temporaryDirectory, file_name)
        temporary_file_2 = os.path.join(self.temporaryDirectory, 'z' + file_name)
        # wczytanie i b-w
        image = cv2.imread(file_path)
        gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
        cv2.imwrite(gray_file_path, gray_image)

        # resize b-w
        im = Image.open(gray_file_path)
        im.thumbnail(size)
        im.save(temporary_file_2)

        imb = cv2.imread(temporary_file_2)
        ret, binary_image = cv2.threshold(imb, 100, 255, cv2.THRESH_BINARY)
        return binary_image, file_name