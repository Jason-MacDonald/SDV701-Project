INSERT INTO category
VALUES 
	('Mountain E-Bikes', 'This is the decription for Mountain E-Bikes.'),
	('Commuter E-Bikes', 'This is the description fro Commuter E-Bikes.'),
    ('Leisure and Trail E-Bikes', 'This is the description for Leisure and Trail E-Bikes.'),
    ('Specialty, Folding E-Bikes and Scooters', 'This is the description for Specialty, Folding E-Bikes and Scooters.')
;

INSERT INTO item
VALUES
	(1, 'Commuter E-Bikes', NULL, 'Magnum Voyager Electric Bike', 'This is the decription for the Magnum Voyager Electric Bike.', 2399.00, '2020-01-01', 10, 'Das-Kit 48V hub-drive motor', 6, NULL),
	(2, 'Commuter E-Bikes', NULL, 'Black City Electric Bike', 'This is the decription for the Black City Electric Bike.', 1699.00, '2020-02-02', 5, 'X2 hub-drive motor', NULL, 'Fair'),
    (3, 'Leisure and Trail E-Bikes', NULL, 'Rev Bikes Coaster', 'This is the decription for the Rev Bikes Coaster.', 3100.00, '2020-03-03', 1, 'NA', 6, NULL)
;

INSERT INTO itemorder
VALUES
	(1, 1, 5, 2399.00, 'John Doe', 'john-doe@email.com'),
    (2, 2, 1, 1699.00, 'Jane Doe', 'jane-doe@email.com')
;