from configuration_global.logger_factory import LoggerFactory


class KerasNeuralNetworkRunner():
    def __init__(self):
        self.logger = LoggerFactory()

    def run(self):
        self.logger.info("Starting run")


if __name__ == "__main__":
    runner = KerasNeuralNetworkRunner()
    runner.run()
