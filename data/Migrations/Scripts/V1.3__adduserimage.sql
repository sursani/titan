CREATE TABLE "UserImage" (
    "Id" serial NOT NULL,
    "IsDefault" bool NOT NULL,
    "Name" text NULL,
    "UserId" int4 NULL,
    CONSTRAINT "PK_UserImage" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_UserImage_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id") ON DELETE NO ACTION
);

CREATE INDEX "IX_UserImage_UserId" ON "UserImage" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20171227215947_AddUserImage', '2.0.0-rtm-26452');

