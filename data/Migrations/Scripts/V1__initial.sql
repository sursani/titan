CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" varchar(150) NOT NULL,
    "ProductVersion" varchar(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

CREATE TABLE "User" (
    "Id" serial NOT NULL,
    "DateOfBirth" timestamp NOT NULL,
    "Email" text NULL,
    "FirstName" text NULL,
    "Gender" int4 NOT NULL,
    "LastName" text NULL,
    "UserName" text NULL,
    CONSTRAINT "PK_User" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20171124164204_Initial', '2.0.0-rtm-26452');

