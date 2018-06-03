from configuration_global.logger_factory import LoggerFactory
from core.neural_network_tester import NeuralNetworkTester
from core.neural_network_trainer import NeuralNetworkTrainer


class KerasNeuralNetworkRunner():
    def __init__(self):
        self.logger = LoggerFactory()
        self.nnTrainer = NeuralNetworkTrainer()
        self.nnTester = NeuralNetworkTester()

    def run(self):
        self.logger.info("Starting run")
        self.nnTrainer.train_neural_network("test2")
        self.nnTester.test_neural_network("test2")


if __name__ == "__main__":
    runner = KerasNeuralNetworkRunner()
    runner.run()
