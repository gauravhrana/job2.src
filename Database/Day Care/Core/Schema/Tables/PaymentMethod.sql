IF OBJECT_Id ('dbo.PaymentMethod') IS NOT NULL
PRINT 'Dropping Table PaymentMethod'
   DROP TABLE dbo.PaymentMethod
GO
PRINT 'Creating Table PaymentMethod'
CREATE TABLE dbo.PaymentMethod
(
        PaymentMethodId		INT            NOT NULL
	,	Name				VARCHAR(50)    NOT NULL
	,	Description		    VARCHAR(500)   NOT NULL
	,	SortOrder			INT			   NOT NULL
)
GO