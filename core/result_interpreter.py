from configuration_global.logger_factory import LoggerFactory


class ResultInterpreter():
    def __init__(self):
        self.logger = LoggerFactory()

    def compare_result(self, file_name, actual_result, expected_value):
        if actual_result[0][0] == 1:
            prediction = 'dog'
        else:
            prediction = 'cat'
        is_answer_correct = prediction == expected_value
        self.logger.info(
            f"Tested file: {file_name}"
            f"\nResult value: {nn_result[0][0]} \nResult: {prediction} "
            f"\nExpected: {expected_value} \nAnswer is {is_answer_correct} \n \n")
