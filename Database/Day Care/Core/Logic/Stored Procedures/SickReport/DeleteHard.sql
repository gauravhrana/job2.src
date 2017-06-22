IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SickReportDeleteHard'
	DROP  Procedure SickReportDeleteHard
END
GO

PRINT 'Creating Procedure SickReportDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: SickReportDelete
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

CREATE Procedure dbo.SickReportDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'SickReport'	
)
AS
BEGIN

	IF @KeyType = 'SickReportId'
		BEGIN

			DELETE	 dbo.SickReport
			WHERE	 SickReportId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.SickReport
			WHERE	 StudentId = @KeyId

		END	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'SickReport'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
