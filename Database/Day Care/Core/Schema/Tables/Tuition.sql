IF OBJECT_Id ('dbo.Tuition') IS NOT NULL
PRINT 'Dropping Table Tuition'
   DROP TABLE dbo.Tuition
GO
PRINT 'Creating Table Tuition'
CREATE TABLE dbo.Tuition 
(
		TuitionId            INT      NOT NULL
    ,	StudentId            INT      NOT NULL
    ,	TuitionDueDate       DATETIME NOT NULL
    ,	TuitionAmount        FLOAT    NOT NULL
    ,	DiscountId           INT      NULL
    ,	DiscountAmount       FLOAT    NULL
    ,	TuitionAmountDue     FLOAT    NULL
    ,	PaymentMethodId      INT      NOT NULL
    ,	TuitionPaymentAmount FLOAT    NULL
)
GO

