CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS restaurants(
        id INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
        name VARCHAR(255) NOT NULL,
        imgUrl VARCHAR(255) NOT NULL,
        description VARCHAR(500) NOT NULL,
        visits INT DEFAULT 0,
        isShutdown TINYINT DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

    INSERT INTO restaurants
    (name, imgUrl, description, creatorId)
    VALUES
    ('Bangkok Thai 3', 'https://images.unsplash.com/photo-1637806930600-37fa8892069d?auto=format&fit=crop&q=80&w=1970&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Like the second one, but the best one. Just look at that high resolution image of food. I must legally say this is not a dish we serve at our restaurant but is a thai dish that represents the style of food we serve.', '651d851e2340a4e22f5d7417');

    -- REPORTS

    CREATE TABLE reports(
        id INT AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(500) NOT NULL,
        body VARCHAR(1000) NOT NULL,
        pictureOfDisgust VARCHAR(1000),
        creatorId VARCHAR(255) NOT NULL,
        restaurantId INT NOT NUll,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (restaurantId) REFERENCES restaurants(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

    INSERT INTO reports
    (title, body, `creatorId`, `restaurantId`)
    VALUES
    ("Love it","Can't get better than the second, the third always is a let down in a trilogy", '6526c4304dedc9f41b528495', 3);

      SELECT
          reports.*,
          repCreator.*,
          restaurants.*,
          restCreator.*
        FROM reports
          JOIN accounts repCreator ON repCreator.id = reports.creatorId
          JOIN restaurants ON restaurants.id = reports.restaurantId
          JOIN accounts restCreator ON restCreator.id = restaurants.creatorId
        WHERE reports.restaurantId = 7;


SELECT
    restaurants.*,
    COUNT(reports.id) as reportCount
FROM restaurants
LEFT JOIN reports ON reports.restaurantId  = restaurants.id
GROUP BY (restaurants.id);

  SELECT
            restaurants.*,
            COUNT(reports.id) AS reportCount,
            accounts.*
        FROM restaurants
        LEFT JOIN reports ON  reports.restaurantId = restaurants.id
        JOIN accounts ON accounts.id = restaurants.creatorId
        GROUP BY (restaurants.id);
