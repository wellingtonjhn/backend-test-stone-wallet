CREATE TABLE USERS
(
	ID UUID PRIMARY KEY
	, NAME VARCHAR(100) NOT NULL
	, EMAIL VARCHAR(40) NOT NULL UNIQUE
	, PASSWORD VARCHAR(255) NOT NULL
	, CREATIONDATE TIMESTAMP NOT NULL
);

CREATE TABLE WALLETS
(
	ID UUID PRIMARY KEY
	, USERID UUID NOT NULL UNIQUE
	, WALLETLIMIT DECIMAL
	, CREATIONDATE TIMESTAMP NOT NULL 
	, CONSTRAINT FK_WALLET_USERS FOREIGN KEY (USERID) REFERENCES USERS (ID)
);

CREATE TABLE CREDITCARDS
(
	ID UUID PRIMARY KEY
	, WALLETID UUID NOT NULL
	, NUMBER BIGINT NOT NULL
	, DUEDATE DATE NOT NULL
	, EXPIRATIONDATE DATE NOT NULL
	, PRINTEDNAME VARCHAR(100) NOT NULL
	, CVV INT NOT NULL
	, CREDITLIMIT DECIMAL NOT NULL
	, AVAILABLECREDIT DECIMAL NOT NULL
	, CREATIONDATE TIMESTAMP NOT NULL 
	, CONSTRAINT FK_CREDITCARD_WALLET FOREIGN KEY (WALLETID) REFERENCES WALLETS (ID)
);
