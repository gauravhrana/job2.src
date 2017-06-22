IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiscountDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DiscountDeleteHard'
	DROP  Procedure DiscountDeleteHard
END
GO

PRINT 'Creating Procedure DiscountDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: DiscountDeleteHard
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.DiscountDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT							
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'Discount'
)
AS
BEGIN

	IF @KeyType = 'DiscountId'
		BEGIN
					 
		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'DiscountId'
		,    	@AuditId 	=	@AuditId
		

		DELETE	 dbo.Discount
		WHERE	 DiscountId = @KeyId

	END

	
END
GO
