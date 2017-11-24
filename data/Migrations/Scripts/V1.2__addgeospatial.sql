ALTER TABLE "User" ADD "Created" timestamp NOT NULL DEFAULT TIMESTAMP '0001-01-01 00:00:00.0000000';

ALTER TABLE "User" ADD "Location" GEOGRAPHY(Point) NULL;

ALTER TABLE "User" ADD "Updated" timestamp NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20171124222535_AddGeospatial', '2.0.0-rtm-26452');

