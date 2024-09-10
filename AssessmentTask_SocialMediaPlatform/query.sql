INSERT INTO Users (UserName) VALUES ('Alice');
INSERT INTO Users (UserName) VALUES ('Bob');

INSERT INTO Posts (UserID, Content) VALUES (1, 'Hello world!');
INSERT INTO Posts (UserID, Content) VALUES (2, 'My first post.');

INSERT INTO Likes (UserID, PostID) VALUES (1, 2);
INSERT INTO Likes (UserID, PostID) VALUES (2, 1);

INSERT INTO Comments (UserID, PostID, Content) VALUES (1, 2, 'Nice post!');
INSERT INTO Comments (UserID, PostID, Content) VALUES (2, 1, 'Thanks!');
