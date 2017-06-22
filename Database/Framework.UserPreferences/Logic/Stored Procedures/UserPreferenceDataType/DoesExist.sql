IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserPreferenceDataTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeDoesExist'
	DROP  Procedure  UserPreferenceDataTypeDoesExist
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.UserPreferenceDataTypeDoesExist
(
		@UserPreferenceDataTypeId		INT				= NULL	
	,	@ApplicationId					INT	
	,	@Name							VARCHAR(50)		
	,	@AuditId						INT							
	,	@AuditDate						DATETIME		= NULL		
	,	@SystemEntityType				VARCHAR(50)		= 'UserPreferenceDataType'	
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.UserPreferenceDataType a
	WHERE		a.Name			=	@Name	
	AND			a.ApplicationId	=	a.ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
	    	@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

