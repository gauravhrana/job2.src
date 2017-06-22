IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportDelete')
BEGIN
	PRINT 'Dropping Procedure SickReportDelete'
	DROP  Procedure  SickReportDelete
END
GO

PRINT 'Creating Procedure SickReportDelete'
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

CREATE Procedure dbo.SickReportDelete
(
		@SickReportId		INT
	,	@ApplicationId		INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME = NULL
	,   @SystemEntityType	VARCHAR(50)	= 'SickReport' 
)
AS
 BEGIN
	DELETE	dbo.SickReport
	WHERE	SickReportId = @SickReportId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SickReportId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

