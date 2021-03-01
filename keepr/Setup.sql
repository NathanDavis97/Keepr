
-- CREATE TABLE profiles
-- (
--   id VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   name VARCHAR(255),
--   picture VARCHAR(255),
--   PRIMARY KEY (id)
-- );


CREATE TABLE keeps
(
  id INT NOT NULL AUTO_INCREMENT,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL ,
  description VARCHAR(255) NOT NULL,
  img VARCHAR(255) NOT NULL,
  keeps INT NOT NULL,
  views INT NOT NULL,

  PRIMARY KEY (id),

  FOREIGN KEY (creatorId)
   REFERENCES profiles (id)
   ON DELETE CASCADE
);

-- DROP TABLE keeps
-- DROP TABLE keep

-- CREATE TABLE reviews
-- (
--    id INT NOT NULL AUTO_INCREMENT,
--    restaurantId INT NOT NULL,
--    title VARCHAR(255) NOT NULL,
--    body VARCHAR(255) NOT NULL,
--    creatorId VARCHAR(255) NOT NULL,
--    published TINYINT NOT NULL DEFAULT 0,
--    rating INT CHECK (rating >= 0 AND rating < 6),

--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--    REFERENCES profiles(id)
--    ON DELETE CASCADE,

--   FOREIGN KEY (restaurantId)
--    REFERENCES restaurants(id)
--    ON DELETE CASCADE
-- );
