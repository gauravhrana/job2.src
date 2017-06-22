IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonXApplicationRoleList')
	BEGIN
		PRINT 'Dropping Procedure PersonXApplicationRoleList'
		DROP  Procedure  PersonXApplicationRoleList
	END

GO

PRINT 'Creating Procedure PersonXApplicationRoleList'
GO


/******************************************************************************
**		File: 
**		Name: PersonXApplicationRole_List
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure PersonXApplicationRoleList
(
		@PersonId					INT = NULL
	,	@ApplicationRoleId			INT = NULL
)
AS
BEGIN
		SELECT *
		FROM	dbo.PersonXApplicationRole
		WHERE	PersonId				= ISNULL(@PersonId, PersonId)
		AND		ApplicationRoleId		= ISNULL(@ApplicationRoleId, ApplicationRoleId)
END

GO

