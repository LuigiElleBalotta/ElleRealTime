-- Insert 2 default accounts:
-- 1 -> Username: admin, Password: admin
-- 2 -> Username: test, Password: test
INSERT INTO accounts(Username, Password) VALUES ('admin', '8301316D0D8448A34FA6D0C6BF1CBFA2B4A1A93A');
INSERT INTO accounts(Username, Password) VALUES ('test', '3D0D99423E31FCC67A6745EC89D70D700344BC76');

INSERT INTO creatures_template(PrefabName, Name) VALUES ('Dummy', 'Dummy');
INSERT INTO creatures_template(PrefabName, Name) VALUES ('SuperZombie', 'SuperZombie');