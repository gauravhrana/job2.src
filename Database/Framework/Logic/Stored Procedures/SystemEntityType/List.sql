IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'SystemEntityTypeList')
BEGIN
	PRINT 'Dropping Procedure SystemEntityTypeList'
	DROP  Procedure  dbo.SystemEntityTypeList
END
GO

PRINT 'Creating Procedure SystemEntityTypeList'
GO

/******************************************************************************
**		File: 
**		EntityName: SystemEntityTypeList
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
**		Date:		Author:				EntityDescription:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SystemEntityTypeList
(
		@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityType'
)
AS
BEGIN

	SELECT	SystemEntityTypeId		
		,	EntityName				
		,	EntityDescription		
		,	NextValue				
		,	IncreaseBy	
		,	PrimaryDatabase
		,	CreatedDate		
		,	SystemEntityTypeId	AS	'SystemEntityId'
		,	EntityName			AS	'Name'
	FROM	 dbo.SystemEntityType 
	ORDER BY SystemEntityTypeId				ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO