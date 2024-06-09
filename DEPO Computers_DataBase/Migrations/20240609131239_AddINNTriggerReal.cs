using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPO_Computers_DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddINNTriggerReal : Migration
    {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.Sql(@"
            CREATE TRIGGER trg_CheckINN_BEFORE_INSERT
BEFORE INSERT ON Users
FOR EACH ROW
BEGIN
    DECLARE INNstring CHAR(12);
    DECLARE dgt11 INT;
    DECLARE dgt12 INT;
    DECLARE i INT;

    SET INNstring = NEW.INN;

    -- Проверка на длину строки
    IF CHAR_LENGTH(INNstring) != 12 THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'INN must be 12 characters long';
    END IF;

    -- Проверка специального случая
    IF INNstring = '123' THEN
        LEAVE BEGIN;
    END IF;

    -- Проверка на то, что строка состоит только из цифр
    IF NOT INNstring REGEXP '^[0-9]+$' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'INN must consist of digits only';
    END IF;

    -- Расчет контрольных цифр
    SET dgt11 = (
        (7 * SUBSTRING(INNstring, 1, 1))
        + (2 * SUBSTRING(INNstring, 2, 1))
        + (4 * SUBSTRING(INNstring, 3, 1))
        + (10 * SUBSTRING(INNstring, 4, 1))
        + (3 * SUBSTRING(INNstring, 5, 1))
        + (5 * SUBSTRING(INNstring, 6, 1))
        + (9 * SUBSTRING(INNstring, 7, 1))
        + (4 * SUBSTRING(INNstring, 8, 1))
        + (6 * SUBSTRING(INNstring, 9, 1))
        + (8 * SUBSTRING(INNstring, 10, 1))
    ) % 11 % 10;

    SET dgt12 = (
        (3 * SUBSTRING(INNstring, 1, 1))
        + (7 * SUBSTRING(INNstring, 2, 1))
        + (2 * SUBSTRING(INNstring, 3, 1))
        + (4 * SUBSTRING(INNstring, 4, 1))
        + (10 * SUBSTRING(INNstring, 5, 1))
        + (3 * SUBSTRING(INNstring, 6, 1))
        + (5 * SUBSTRING(INNstring, 7, 1))
        + (9 * SUBSTRING(INNstring, 8, 1))
        + (4 * SUBSTRING(INNstring, 9, 1))
        + (6 * SUBSTRING(INNstring, 10, 1))
        + (8 * SUBSTRING(INNstring, 11, 1))
    ) % 11 % 10;

    -- Проверка контрольных цифр
    IF (SUBSTRING(INNstring, 11, 1) != dgt11) OR (SUBSTRING(INNstring, 12, 1) != dgt12) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid INN';
    END IF;
END;

        ");
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.Sql("DROP TRIGGER IF EXISTS trg_CheckINN;");
            }
        }
}
