IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PaymentMethodDeleteHard')
BEGIN
	PRINT 'Dropping Procedure PaymentMethodDeleteHard'
	DROP  Procedure PaymentMethodDeleteHard
END
GO

PRINT 'Creating Procedure PaymentMethodDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: PaymentMethodDeleteHard
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
**     ----------							-----------
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

CREATE Procedure dbo.PaymentMethodDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'PaymentMethod'	
)
AS

BEGIN

	IF @KeyType = 'PaymentMethodId'
		BEGIN 

			EXEC	dbo.TuitionDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'PaymentMethodId'
				,	@AuditId 	=	@AuditId
	
			DELETE	dbo.PaymentMethod
			WHERE	PaymentMethodId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType			= 'PaymentMethod'
		,	@EntityKey					= @KeyId
		,	@AuditAction				= 'DeleteHard'
		,	@CreatedDate				= @AuditDate
		,	@CreatedByPersonId			= @AuditId
		
END
GO
