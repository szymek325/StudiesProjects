from configuration_global.config_reader import ConfigReader
from configuration_global.logger_factory import LoggerFactory


class NeuralNetworkTester():
    def __init__(self):
        self.logger = LoggerFactory()
        self.config = ConfigReader()

    def test_neural_network(self, nn_name="weights-classifier-cnn.hdf5"):
        self.logger.info("Running neural network tests")
