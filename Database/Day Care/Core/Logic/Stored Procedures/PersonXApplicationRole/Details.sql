IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonXApplicationRoleDetails')
	BEGIN
		PRINT 'Dropping Procedure PersonXApplicationRoleDetails'
		DROP  Procedure  PersonXApplicationRoleDetails
	END

GO

PRINT 'Creating Procedure PersonXApplicationRoleDetails'
GO


/******************************************************************************
**		File: 
**		Name: PersonXApplicationRole_Details
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
CREATE Procedure dbo.PersonXApplicationRoleDetails
(
		@PersonId			INT = NULL
	,	@ApplicationRoleId	INT = NULL
)
AS
BEGIN
		SELECT		a.PersonId
				,	a.ApplicationRoleId
				,	b.FirstName + ' ' + b.LastName AS 'FullName'
				,	c.Name						   AS 'RoleName'	
	
		FROM	dbo.PersonXApplicationRole a
				INNER JOIN dbo.Person b ON a.PersonId = b.PersonId
				INNER JOIN dbo.ApplicationRole c ON a.ApplicationRoleId = c.ApplicationRoleId
		WHERE	a.PersonId	= ISNULL(@PersonId, a.PersonId)
		AND		a.ApplicationRoleId	= ISNULL(@ApplicationRoleId, a.ApplicationroleId)
END

GO

