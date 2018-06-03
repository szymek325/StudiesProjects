import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from core.cnn_creator import CnnCreator
from core.converted_image_provider import ConvertedImageProvider
from core.directory_manager import DirectoryManager
from core.result_interpreter import ResultInterpreter


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
        cats_path = os.path.join(self.config.test_data_path, "cat")
        dogs_path = os.path.join(self.config.test_data_path, "dog")
        cats = self.directoryManager.get_files_from_directory(cats_path)
        dogs = self.directoryManager.get_files_from_directory(dogs_path)
        for cat in cats:
            result = self.__test__image__(cat, classifier, "cat")
            self.resultInterpreter.compare_result(cat, result, "cat")
        for dog in dogs:
            result = self.__test__image__(dog, classifier, "dog")
            self.resultInterpreter.compare_result(dog, result, "dog")

    def __test__image__(self, file_name, classifier):
        test_image = self.imagesProvider.get_image(file_name)
        result = classifier.predict(test_image, verbose='0')
        return result

    def __load_neural_network__(self, nn_to_load):
        classifier = self.cnnCreator.get_neural_network()
        path_to_nn_weights = os.path.join(self.config.neural_networks_path, f"{nn_to_load}.hdf5")
        classifier.load_weights(path_to_nn_weights)
        return classifier

