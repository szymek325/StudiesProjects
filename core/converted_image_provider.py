from configuration_global.config_reader import ConfigReader
from keras.preprocessing import image
import numpy as np


class ConvertedImageProvider():
    def __init__(self):
        self.config = ConfigReader()

    def get_image(self, path_to_file):
        """

        :rtype: image converted to numpy array
        """
        test_image = image.load_img(path_to_file, target_size=(24, 24))
        test_image = image.img_to_array(test_image)
        test_image = np.expand_dims(test_image, axis=0)

        return test_image
