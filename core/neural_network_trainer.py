import os

from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory
from core.cnn_creator import CnnCreator
from core.data_provider import DataProvider
from core.directory_manager import DirectoryManager


class NeuralNetworkTrainer():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()
        self.dataProvider = DataProvider()
        self.directoryManager = DirectoryManager()
        self.nnCreator = CnnCreator()

    def train_neural_network(self, nn_name: "weights-classifier-cnn"):
        nn_files = self.directoryManager.get_files_from_directory(self.config.neural_networks_path)
        file_name = f"{nn_name}.hdf5"
        file_names = [file.split('/')[-1] for file in nn_files]
        if file_name in file_names:
            if self.config.overwrite_old_nn:
                self.logger.info(f"Overwriting neural network : {nn_name}")
                self.__train_nn__(nn_name)
            else:
                self.logger.error(
                    f"Neural network called :{nn_name} already exists. Overwrite is not enabled in cofnig. Skipping training")
        else:
            self.logger.info(f"Training neural network : {nn_name}")
            self.__train_nn__(nn_name)

    def __train_nn__(self, new_nn_name):
        classifier = self.nnCreator.get_neural_network()
        training_set = self.dataProvider.get_training_data_set()
        test_set = self.dataProvider.get_test_data_set()
        classifier.fit_generator(training_set,
                                 steps_per_epoch=1000,
                                 epochs=2,
                                 validation_data=test_set,
                                 validation_steps=1000)

        save_path = os.path.join(self.config.neural_networks_path, f"{new_nn_name}.hdf5")
        classifier.save_weights(save_path, overwrite=True)
