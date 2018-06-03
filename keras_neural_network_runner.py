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


if __name__ == "__main__":
    runner = KerasNeuralNetworkRunner()
    runner.run()
