from configuration_global.logger_factory import LoggerFactory
from core.neural_network_tester import NeuralNetworkTester


class TestingRunner():
    def __init__(self):
        self.logger = LoggerFactory()
        self.nnTester = NeuralNetworkTester()

    def run(self):
        self.logger.info("Starting run")
        self.nnTester.test_neural_network("test4")


if __name__ == "__main__":
    runner = TestingRunner()
    runner.run()
