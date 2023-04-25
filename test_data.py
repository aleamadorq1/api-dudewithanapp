import random
from datetime import date, timedelta

start_date = date(2022, 1, 1)
end_date = date(2023, 4, 18)
delta = timedelta(days=1)

quote_ids = [1, 2, 3, 4, 5, 6]
request_id = "Generated test data by Chatgpt"

current_date = start_date
sql_statements = []

while current_date <= end_date:
    num_quotes = random.randint(0,15)
    for _ in range(num_quotes):
        quote_id = random.choice(quote_ids)
        sql_statements.append(f"INSERT INTO QuotePrints (QuoteId, PrintedAt, RequestId) VALUES ({quote_id}, '{current_date}', '{request_id}');")
    current_date += delta

# Save the generated SQL statements to a file
with open("quoteprints_test_data.sql", "w") as f:
    f.write("\n".join(sql_statements))
