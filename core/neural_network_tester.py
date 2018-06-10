import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from core.cnn_creator import CnnCreator
from core.converted_image_provider import ConvertedImageProvider
from core.directory_manager import DirectoryManager
from core.result_interpreter import ResultInterpreter
import numpy as np


class NeuralNetworkTester():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.cnnCreator = CnnCreator()
        self.imagesProvider = ConvertedImageProvider()
        self.directoryManager = DirectoryManager()
        self.resultInterpreter = ResultInterpreter()

    def test_neural_network(self, nn_name="weights-classifier-cnn"):
        self.logger.info("Running neural network tests")
        classifier = self.__load_neural_network__(nn_name)
        positives = self.directoryManager.get_all_images_from_all_subdirectories(self.config.positive_testing_data_path)
        negatives = self.directoryManager.get_all_images_from_all_subdirectories(self.config.negative_testing_data_path)
        for positive in positives:
            result = self.__test__image__(positive, classifier)
            self.resultInterpreter.compare_result(positive, result, "positive")
        for negative in negatives:
            result = self.__test__image__(negative, classifier)
            self.resultInterpreter.compare_result(negative, result, "negative")
        self.resultInterpreter.get_final_result()

    def __test__image__(self, file_name, classifier):
        test_image = self.imagesProvider.get_image(file_name)
        height, width= test_image.shape[1:3]
        print(height)
        print(width)
        for i in range(0, height-23):
            for j in range(0, width-23):
                subimage = test_image[:, i:i+24, j:j+24, :]
                print(subimage)
                result = classifier.predict(subimage, verbose='0')
                if result[0][0] == 1:
                    print("Eye found. Location: {0} x {1}".format(i, j))
                    return result
        print("Eye not found")
        return result

    def __load_neural_network__(self, nn_to_load):
        classifier = self.cnnCreator.get_neural_network()
        path_to_nn_weights = os.path.join(self.config.neural_networks_path, f"{nn_to_load}.hdf5")
        classifier.load_weights(path_to_nn_weights)
        return classifier
