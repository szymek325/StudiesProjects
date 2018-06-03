from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from core.cnn_creator import CnnCreator
from core.data_provider import DataProvider
from core.directory_manager import DirectoryManager


class NeuralNetworkManager():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.dataProvider = DataProvider()
        self.directoryManager = DirectoryManager()
        self.nnCreator = CnnCreator()

    def train_neural_network(self):
        nn_files = self.directoryManager.get_files_from_directory(self.config.neural_networks_path)
        if self.config.pre_trained_neural_network_name in nn_files:
            if self.config.overwrite_old_nn:
                self.__train_nn__()
        else:
            self.__train_nn__()

    def __train_nn__(self):
        classifier = self.nnCreator.get_neural_network()
        training_set = self.dataProvider.get_training_data_set()
        test_set = self.dataProvider.get_test_data_set()
        classifier.fit_generator(training_set,
                                 steps_per_epoch=8000,
                                 epochs=2,
                                 validation_data=test_set,
                                 validation_steps=2000)

        fname = "weights-classifier-cnn.hdf5"
        classifier.save_weights(fname, overwrite=True)
