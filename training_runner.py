from core.training_data_service import TrainingDataService


class TrainingRunner():
    def __init__(self):
        self.trainingDataService = TrainingDataService()

    def run(self):
        self.trainingDataService.prepare_training_data()


if __name__ == "__main__":
    runner = TrainingRunner()
    runner.run()
