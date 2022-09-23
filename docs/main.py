import psycopg2
from faker import *
import random 

class FootballDB:

    def __init__(self):
        try:
            self.__connection = psycopg2.connect(
                host     = 'localhost', 
                user     = 'postgres', 
                password = '1801', 
                database = 'cars'
                )
            self.__connection.autocommit = True
            self.__cursor = self.__connection.cursor()
            print("PostgreSQL connection opened ✅\n")

        except Exception as err:
            print("Error while working with PostgreSQL ❌\n", err)
            return

    def __del__(self):
        if self.__connection:
            self.__cursor.close()
            self.__connection.close()
            print("PostgreSQL connection closed ✅\n")

    def drop_tables(self):
        try:
            self.__cursor.execute(
                """
                --sql
                
                DROP TABLE public."Cars", public."Categories", public."UserCars", public."Users", public."__EFMigrationsHistory"
                """
            )
            print("Tables have been deleted ✅\n")

        except Exception as err:
            print("Error while working with PostgreSQL ❌\n", err)

    def copy_data(self):
        self.__cursor.execute(
                """
                --sql
                
                copy public."Cars"("Brand", "Model", "Price", "CategoryId") from '/Users/Shared/cars/tables/cars.csv'       delimiter ',' csv;
                copy public."Categories"("Name", "Description")             from '/Users/Shared/cars/tables/categories.csv' delimiter ',' csv;
                copy public."Users"("Login", "Password", "Permission")      from '/Users/Shared/cars/tables/users.csv'      delimiter ',' csv;
                """
            )

if __name__ == "__main__":
    db = FootballDB()

    # db.drop_tables()
    # db.copy_data()
