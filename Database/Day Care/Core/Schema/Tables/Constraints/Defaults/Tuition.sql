IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'TuitionDueDate'
)
ALTER TABLE Tuition
ADD CONSTRAINT DF_Tuition_TuitionDueDate        DEFAULT ''    FOR TuitionDueDate                
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'TuitionAmount'
)
ALTER TABLE Tuition
ADD CONSTRAINT DF_Tuition_TuitionAmount        DEFAULT 100    FOR TuitionAmount                
GO
IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'DiscountAmount'
)
ALTER TABLE Tuition
ADD CONSTRAINT DF_Tuition_DiscountAmount        DEFAULT 100    FOR DiscountAmount               
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'TuitionAmountDue'
)
ALTER TABLE Tuition
ADD CONSTRAINT DF_Tuition_TuitionAmountDue DEFAULT 100    FOR TuitionAmountDue              
GO

IF NOT EXISTS 
(
            SELECT name 
            FROM   sys.objects 
            WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
            AND       name            = 'TuitionPaymentAmount'
)
ALTER TABLE Tuition
ADD CONSTRAINT DF_Tuition_TuitionPaymentAmount DEFAULT 100 FOR TuitionPaymentAmount              
GO


-- Confirmation
SELECT name, * 
FROM   sys.objects 
WHERE type_desc           = 'DEFAULT_CONSTRAINT' 
AND       name   LIKE 'DF_Tuition%'


