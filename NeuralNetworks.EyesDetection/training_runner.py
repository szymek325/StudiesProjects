from configuration_global.logger_factory import LoggerFactory
from core.neural_network_trainer import NeuralNetworkTrainer


class TrainingRunner():
    def __init__(self):
        self.logger = LoggerFactory()
        self.nnTrainer = NeuralNetworkTrainer()

    def run(self):
        self.logger.info("Starting training process")
        self.nnTrainer.train_neural_network("test4")


if __name__ == "__main__":
    runner = TrainingRunner()
    runner.run()
