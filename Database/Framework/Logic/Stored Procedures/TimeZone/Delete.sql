IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneDelete')
BEGIN
	PRINT 'Dropping Procedure TimeZoneDelete'
	DROP  Procedure TimeZoneDelete
END
GO

PRINT 'Creating Procedure TimeZoneDelete'
GO
/******************************************************************************
**		File: 
**		Name: TimeZoneDelete
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TimeZoneDelete
(
		@TimeZoneId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TimeZone'
)
AS
BEGIN

	DELETE	 dbo.TimeZone
	WHERE	 TimeZoneId = @TimeZoneId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TimeZone'
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
