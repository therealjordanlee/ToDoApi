# ToDo API

For use with Angular ToDo test application.
This is a simple API for managing a list of ToDo items.

A ToDo item in this case consists of 3 fields:
```
{
  "summary": "Do Groceries",
  "description": "Buy bread, ham and cheese",
  "completed": false
}
```

## Usage:

| Action | Example Usage |
|--|--|
| **Get all ToDo items:** | `curl -X GET "https://localhost:5001/todo" -H "accept: */*"` |
| **Get a specific ToDo item:** | `curl -X GET "https://localhost:5001/todo/4" -H "accept: */*"` |
| **Create a new ToDo item** | `curl -X POST "https://localhost:5001/todo" -H "accept: */*" -H "Content-Type: application/json" -d "{\"summary\":\"Do some shopping\",\"description\":\"Buy some new pants, a shirt and some shoes\",\"completed\":false}"` |
| **Update an existing ToDo item** | `curl -X PUT "https://localhost:5001/todo/6" -H "accept: */*" -H "Content-Type: application/json" -d "{\"summary\":\"Do some shopping\",\"description\":\"Buy some new pants, a shirt and some shoes\",\"completed\":true}"` |
| **Delete an existing ToDo item** | `curl -X DELETE "https://localhost:5001/todo/6" -H "accept: */*"` |


