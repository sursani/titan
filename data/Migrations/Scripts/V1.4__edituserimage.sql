ALTER TABLE "UserImage" ADD "Created" timestamp NOT NULL;

ALTER TABLE "UserImage" ADD "Updated" timestamp NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20171228153420_EditUserImage', '2.0.0-rtm-26452');

