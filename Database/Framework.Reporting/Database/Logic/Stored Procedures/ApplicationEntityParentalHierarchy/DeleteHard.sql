IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyDeleteHard'
	DROP  Procedure ApplicationEntityParentalHierarchyDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationEntityParentalHierarchyDelete
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
CREATE Procedure dbo.ApplicationEntityParentalHierarchyDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN

	IF @KeyType = 'ApplicationEntityParentalHierarchyId'
	BEGIN

		DELETE	 dbo.ApplicationEntityParentalHierarchy
		WHERE	 ApplicationEntityParentalHierarchyId = @KeyId
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
