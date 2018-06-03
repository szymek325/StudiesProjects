from configuration_global.logger_factory import LoggerFactory


class ResultInterpreter():
    def __init__(self):
        self.logger = LoggerFactory()
        self.correct = 0
        self.error = 0

    def compare_result(self, file_name, actual_result, expected_value):
        if actual_result[0][0] == 1:
            prediction = 'other'
        else:
            prediction = 'eye'
        is_answer_correct = prediction == expected_value
        if is_answer_correct:
            self.correct = self.correct + 1
        else:
            self.error = self.error + 1
        self.logger.info(
            f"Tested file: {file_name}"
            f"\nResult value: {actual_result[0][0]} \nResult: {prediction} "
            f"\nExpected: {expected_value} \nAnswer is {is_answer_correct} \n \n")

    def get_final_result(self):
        self.logger.info(f"Testing FINISHED \nCorrectly Identified: {self.correct} \nMistakes: {self.error}")
