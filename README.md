# DotNotCoreWebApp_RESTAPI

# .NET Core Web Application - BookStore API

## Project Overview
This project implements a RESTful API using .NET Core for managing a collection of books. It provides endpoints to fetch books sorted by various criteria, calculate the total price of all books, and includes stored procedures for optimized data retrieval. The goal is to demonstrate proficiency in .NET Core MVC, and SQL Server database interactions.

## API Endpoints
- **GET /api/books/sorted-books: Retrieves a sorted list of books by Publisher, Author (last, first), then Title.
- **GET /api/books/sorted-author-books: Retrieves a sorted list of books by Author (last, first) then Title.
- **GET /api/books/insert-books: Insert list of books with Author and Title.
- **GET /api/books/total-price: Retrieves the total price of all books in the database.

## Database
### Database Setup
- **Database Type**: SQL Server
- **Connection String**: Replace `DefaultConnection` in `appsettings.json` with your database connection string.
- **Tables and Procedure** : Tables and Procedures scrpits are in sqlProceduresandtables folder
  
## TESTING IN POSTMAN
- Use BulkInsertPayload.json from payload folder for bulk Insert books
