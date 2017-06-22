IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TuitionDeleteHard'
	DROP  Procedure TuitionDeleteHard
END
GO

PRINT 'Creating Procedure TuitionDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: TuitionDelete
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

CREATE Procedure dbo.TuitionDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Tuition'
)
AS
BEGIN

	IF @KeyType = 'TuitionId'
		BEGIN

			DELETE	 dbo.Tuition
			WHERE	 TuitionId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.Tuition
			WHERE	 StudentId = @KeyId

		END	
	ELSE IF @KeyType = 'PaymentMethodId'
		BEGIN

			DELETE	 dbo.Tuition
			WHERE	 PaymentMethodId = @KeyId

		END	
	ELSE IF @KeyType = 'DiscountId'
		BEGIN

			DELETE	 dbo.Tuition
			WHERE	 DiscountId = @KeyId

		END	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= 'Tuition'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
