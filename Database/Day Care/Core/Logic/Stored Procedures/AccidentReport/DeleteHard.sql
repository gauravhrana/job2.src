IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportDeleteHard')
BEGIN
	PRINT 'Dropping Procedure AccidentReportDeleteHard'
	DROP  Procedure AccidentReportDeleteHard
END
GO

PRINT 'Creating Procedure AccidentReportDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: AccidentReportDelete
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

CREATE Procedure dbo.AccidentReportDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'AccidentReport'	
)
AS
BEGIN

	IF @KeyType = 'AccidentReportId'
		BEGIN

			DELETE	 dbo.AccidentReport
			WHERE	 AccidentReportId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.AccidentReport
			WHERE	 StudentId = @KeyId

		END
	ELSE IF @KeyType = 'AccidentPlaceId'
		BEGIN

			DELETE	 dbo.AccidentReport
			WHERE	 AccidentPlaceId = @KeyId

		END 
	ELSE IF @KeyType = 'TeacherId'
		BEGIN

			DELETE	 dbo.AccidentReport
			WHERE	 TeacherId = @KeyId

		END 
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
