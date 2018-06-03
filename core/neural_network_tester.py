import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from core.cnn_creator import CnnCreator
from core.converted_image_provider import ConvertedImageProvider
from core.directory_manager import DirectoryManager


class NeuralNetworkTester():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.cnnCreator = CnnCreator()
        self.imagesProvider = ConvertedImageProvider()
        self.directoryManager = DirectoryManager()

    def test_neural_network(self, nn_name="weights-classifier-cnn"):
        self.logger.info("Running neural network tests")
        classifier = self.__load_neural_network__(nn_name)
        cats_path = os.path.join(self.config.test_data_path, "cat")
        dogs_path = os.path.join(self.config.test_data_path, "dog")
        cats = self.directoryManager.get_files_from_directory(cats_path)
        dogs = self.directoryManager.get_files_from_directory(dogs_path)
        for cat in cats:
            self.__test__image__(cat, classifier, "cat")
        for dog in dogs:
            self.__test__image__(dog, classifier, "dog")

    def __test__image__(self, cat, classifier, expected):
        test_image = self.imagesProvider.get_image(cat)
        result = classifier.predict(test_image, verbose='0')
        self.__compare_result__(result, expected)

    def __load_neural_network__(self, nn_to_load):
        classifier = self.cnnCreator.get_neural_network()
        path_to_nn_weights = os.path.join(self.config.neural_networks_path, f"{nn_to_load}.hdf5")
        classifier.load_weights(path_to_nn_weights)
        return classifier

    def __compare_result__(self, nn_result, expected_value):
        if nn_result[0][0] == 1:
            prediction = 'dog'
        else:
            prediction = 'cat'
        is_answer_correct = prediction == expected_value
        self.logger.info(
            f"Result value: {nn_result[0][0]} \nResult: {prediction} "
            f"\nExpected: {expected_value} \nAnswer is {is_answer_correct} \n \n")
