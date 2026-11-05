DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'sample') THEN
        CREATE SCHEMA sample;
    END IF;
END $EF$;
CREATE TABLE IF NOT EXISTS sample."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM sample."__EFMigrationsHistory" WHERE "MigrationId" = '20261105084935_InitialCreate') THEN
        IF NOT EXISTS(SELECT 1 FROM pg_namespace WHERE nspname = 'sample') THEN
            CREATE SCHEMA sample;
        END IF;
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM sample."__EFMigrationsHistory" WHERE "MigrationId" = '20261105084935_InitialCreate') THEN
    CREATE TABLE sample.tasks (
        id uuid NOT NULL,
        title character varying(200) NOT NULL,
        description text,
        is_completed boolean NOT NULL,
        created_at timestamp with time zone NOT NULL,
        CONSTRAINT "PK_tasks" PRIMARY KEY (id)
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM sample."__EFMigrationsHistory" WHERE "MigrationId" = '20261105084935_InitialCreate') THEN
    CREATE INDEX ix_tasks_is_completed ON sample.tasks (is_completed);
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM sample."__EFMigrationsHistory" WHERE "MigrationId" = '20261105084935_InitialCreate') THEN
    INSERT INTO sample."__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20261105084935_InitialCreate', '8.0.33');
    END IF;
END $EF$;
COMMIT;

