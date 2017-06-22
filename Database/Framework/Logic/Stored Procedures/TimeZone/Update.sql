IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneUpdate')
BEGIN
	PRINT 'Dropping Procedure TimeZoneUpdate'
	DROP  Procedure  TimeZoneUpdate
END
GO

PRINT 'Creating Procedure TimeZoneUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TimeZoneUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TimeZoneUpdate
(
		@TimeZoneId					INT			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT	
	,	@TimeDifference				DECIMAL(4,2)				
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZone'
)
AS
BEGIN
	UPDATE	dbo.TimeZone 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder	
		,	TimeDifference			=	@TimeDifference						
	WHERE	TimeZoneId	=	@TimeZoneId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TimeZone'
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO