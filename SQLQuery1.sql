CREATE TABLE SeatColor (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Color NVARCHAR(50),         -- Màu của ghế (có thể null)
    Price INT NOT NULL          -- Giá tiền tương ứng màu
);
CREATE TABLE Seat (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RowChar NVARCHAR(5) NOT NULL,         -- Hàng ghế (A, B, C...)
    ColNumber INT NOT NULL,               -- Số cột (1, 2, 3...)
    SeatColorId INT NOT NULL,             -- Khóa ngoại đến SeatColor
    Floor NVARCHAR(50),                   -- Tầng (có thể null)

    FOREIGN KEY (SeatColorId) REFERENCES SeatColor(Id)
);
CREATE TABLE [User] (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255),
    RefreshToken NVARCHAR(500),
    RefreshTokenExpiryTime DATETIME,
    CreatedAt DATETIME DEFAULT GETDATE()
);
Insert Into [User] (Username,PasswordHash,Email,RefreshToken)
Values ('phuc123','123456','lamminhphuc@gmail.com','sJ3mLhb9l5PT2655MzrNPwCwTxQ74gXiH+mEP4sL1g9oFw157BYGGY8A3b2bXQ6ZFKNLLVf0I31AduzgbqBYFw==')