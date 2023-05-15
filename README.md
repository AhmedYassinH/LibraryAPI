




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
  - [Get all books by author](#get-all-books-by-author-and-branch)
    - [Get all books by author Request](#get-all-books-by-author-and-branch-request)
    - [Get all books by author](#get-all-books-by-author-and-branch-response)
  - [Get all books by keyword](#get-all-books-by-keyword)
    - [Get all books by keyword Request](#get-all-books-by-keyword-request)
    - [Get all books by keyword](#get-all-books-by-keyword-response)

- [Borrowers API](#borrowers-api)
  - [Create a Borrower](#create-a-borrower)
    - [Create a Borrower Request](#create-a-borrower-request)
    - [Create a Borrower Response](#create-a-borrower-response)
  - [Get a Borrower](#get-a-borrower)
    - [Get a Borrower Request](#get-a-borrower-request)
    - [Get a Borrower Response](#get-a-borrower-response)
  - [Update Borrower's Address](#update-borrowers-address)
    - [Update Borrower's Address Request](#update-borrowers-address-request)
    - [Update Borrower's Address Response](#update-borrowers-address-response)
    
  - [Delete a Borrower](#delete-a-borrower)
    - [Delete a Borrower Request](#delete-a-borrower-request)
    - [Delete a Borrower Response](#delete-a-borrower-response)
    
- [Loan API](#loan-api)
  - [Make a Loan -Borrow a book-](#make-a-loan-borrow-a-book)
    - [Make a Loan Request](#make-a-loan-request)
    - [Make a Loan Response](#make-a-loan-respone)
  - [Delete a Loan -Return a book-](#delete-a-loan-return-a-book)
    - [Delete a Loan Request](#delete-a-loan-request)
    - [Delete a Loan Response](#delete-a-loan-respone)











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
[
  {
    "BookID": 5,
    "Title": "The Hobbit",
    "PublisherName": "George Allen & Unwin"
  }
]
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
200 Ok
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



### Get all books by author and branch

#### Get all books by author and branch Request
 

```js
GET /book/{{author}}/{{branch}}
```


#### Get all books by author and branch Response

```js
200 Ok
```

```json
{       
  [

    {
      "BookID":2,
      "Title":"It",
      "PublisherName":"Viking"
    },

    {
      "BookID":3,
      "Title":"The Green Mile",
      "PublisherName":"Signet Books"
    }


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
{      

  [
    {
      "BookID": 11,
      "Title": "The Hitchhikers Guide to the Galaxy",
      "PublisherName": "Pan Books"
    },
    {
      "BookID": 44,
      "Title": "The C# Player''s Guide",
      "PublisherName": "Bantam"
    }
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
    "Title": "It",
    "CardNo": 101,
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



### Get all Loans by Borrower card No


#### Get all Loans by Borrower card No Request
 

```js
GET /Loans/{{cardNo}}
```


#### Get all Loans by Borrower card No Response

```js
200 Ok
```

```json
{      

  [
    {
      "LoanID":1,
      "BookID": 11,
      "BranchID":1,
      "CardNo":100,
      "DateOut":"2018-01-01T00:00:00",
      "DueDate":"2018-01-16T00:00:00"
    },
    {
      "LoanID":2,
      "BookID": 2,
      "BranchID":1,
      "CardNo":100,
      "DateOut":"2018-01-01T00:00:00",
      "DueDate":"2018-01-16T00:00:00"
    }
  ]
}
```
