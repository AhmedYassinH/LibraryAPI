




# Library Management System API

- [Books API](#books-api)

  - [Create a Book](#create-a-book)
    -  [Create a book Request](#create-a-book-request)
    -  [Create a book Response](#create-a-book-response)

  - [Get a Book](#get-a-book)
    - [Create a book Request](#get-a-book-request)
    - [Create a book Response](#get-a-book-response)

  - [Update a book](#update-a-book)
    - [Update a book Request](#update-a-book-request)
    - [Update a book Response](#update-a-book-response)
  - [Delete a book](#delete-a-book)
    - [Delete a book Request](#delete-a-book-request)
    - [Delete a book Response](#delete-a-book-response)
  - [Get all book by author](#get-all-books-author-and-branch)
    - [Get all book by author Request](#get-all-books-by-author-and-branch-request)
    - [Get all book by author](#get-all-books-by-author-and-branch-response)
  - [Get all books by keyword](#get-all-books-by-keyword)
    - [Get all books by keyword Request](#get-all-books-by-keyword-request)
    - [Get all books by keyword](#get-all-books-by-keyword-response)


## Books API

### Create a Book

#### Create a Book Request

```js
POST /book
```

```json
{
    "Title": "The C# Player's Guide",
    "PublisherName": "Starbound Software"
}
```



#### Create a Book Response

```js
201 Created
```




### Get a book

#### Get a book Request
 

```js
GET /book/{{id}}
```


#### Get a book Response

```js
200 Ok
```

```json
{
        "BookID" : 5 ,
        "Title": "The C# Player's Guide",
        "PublisherName": "Starbound Software"
}
```




### Update a book

#### Update a book Request

```js
PUT /book/{{id}}
```

```json
{    
    "Title": "The C# Player's Guide 2022",
    "PublisherName": "Starbound Software"
}
```

#### Update a book Response

```js
204 No Content
```




### Delete a book

#### Delete a book Request

```js
DELETE /book/{{id}}
```

#### Delete a book Response

```js
204 No Content
```



### Get all books author and branch

#### Get all books by author and branch Request
 

```js
GET /book/{{author}}/{{branch}}
```


#### Get all books by author and branch Response

```js
200 Ok
```

```json
{       [

// TODO: Add content here similar to the database


        ]
}
```


### Get all books by keyword


#### Get all books by keyword Request
 

```js
GET /book/{{keyword}}
```


#### Get all books by keyword Response

```js
200 Ok
```

```json
{       [

// TODO: Add content here similar to the database


        ]
}
```





## Borrowers API

### Create a borrower

#### Create a borrower Request

```js
POST /borrower
```

```json
{
    "BorrowerName": "John David",
    "BorrowerAddress": "2212 Main Avenue",
    "BorrowerPhone": "456-689-5522"
}
```



#### Create a borrower Response

```js
201 Created
```



### Get a borrower

#### Get a borrower Request
 

```js
GET /borrower/{{cardno}}
```


#### Get a borrower Response

```js
200 Ok
```

```json
{
    "BorrowerName": "John David",
    "BorrowerAddress": "2212 Main Avenue",
    "BorrowerPhone": "456-689-5522"
}
```




### Update borrower's address

#### Update borrower's address Request

```js
PUT /borrower/{{cardno}}
```

```json
{    
    "BorrowerAddress": "2255 Main Avenue"
}
```

#### Update borrower's address Response

```js
204 No Content
```





### Delete a borrower

#### Delete a borrower Request

```js
DELETE /borrower/{{cardno}}
```

#### Delete a borrower Response

```js
204 No Content
```




## Loan API 

### Make a loan (Borrow a book)

#### Make a loan Request

```js
POST /loan
```

```json
{
    "BoookTitle": "It",
    "BorrowerCardNo": 101,
    "BranchID": 1,
    "DueDate": NULL
}
```



#### Make a loan Respone
```js
201 Created
```


### Delete a loan (Return a book)


#### Delete a loan Request
```js
DELETE /loan/{{title}}/{{card_no}}/{{branch_id}}
```
<!-- TODO: Modify or delete the lines below and above -->
```json
{
    "BoookTitle": "It",
    "BorrowerCardNo": 101,
    "BranchID": 1,
}
```


#### Delete a loan Respone

```js
204 No Content
```