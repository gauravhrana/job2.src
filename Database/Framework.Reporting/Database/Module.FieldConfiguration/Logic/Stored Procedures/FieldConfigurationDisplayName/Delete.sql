IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FieldConfigurationDisplayNameDelete')
BEGIN
	PRINT 'Dropping Procedure FieldConfigurationDisplayNameDelete'
	DROP  Procedure FieldConfigurationDisplayNameDelete
END

GO

PRINT 'Creating Procedure FieldConfigurationDisplayNameDelete'
GO


/******************************************************************************
**		File: 
**		Name: FieldConfigurationDisplayNameDelete
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
CREATE Procedure dbo.FieldConfigurationDisplayNameDelete
(
		@FieldConfigurationDisplayNameId 		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'FieldConfigurationDisplayName'
)
AS
BEGIN

	DELETE	 dbo.FieldConfigurationDisplayName
	WHERE	 FieldConfigurationDisplayNameId = @FieldConfigurationDisplayNameId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		=	@SystemEntityType
		,	@EntityKey				=	@FieldConfigurationDisplayNameId
		,	@AuditAction			=	'Delete'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO


