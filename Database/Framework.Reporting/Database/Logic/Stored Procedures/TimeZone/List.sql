IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneList')
BEGIN
	PRINT 'Dropping Procedure TimeZoneList'
	DROP  Procedure  dbo.TimeZoneList
END
GO

PRINT 'Creating Procedure TimeZoneList'
GO

/******************************************************************************
**		File: 
**		Name: TimeZoneList
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
**     ----------					   ---------
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

CREATE Procedure dbo.TimeZoneList
(
		@AuditId				INT	
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TimeZone'
)
AS
BEGIN

	SELECT	TimeZoneId	
		,   ApplicationId   
		,	Name		  	
		,	Description	   
		,	SortOrder		
		,	TimeDifference
	 FROM	dbo.TimeZone 
	WHERE	ApplicationId	=	@ApplicationId

	ORDER BY TimeZoneId			ASC
		,	 SortOrder						ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO